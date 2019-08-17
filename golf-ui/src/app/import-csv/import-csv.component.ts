import { Component, OnInit } from '@angular/core';
import { Order } from '../models/order';
import { forEach } from '@angular/router/src/utils/collection';
import { ComponentOrder } from '../models/component';
import { OrderClientService } from '../services/orderClient.service';
import { User } from '../models/user';
import { Router } from '@angular/router';
import { GlobalService } from '../global.service';
import { LoginResponse } from '../apiModels/login-response';

@Component({
  selector: 'app-import-csv',
  templateUrl: './import-csv.component.html',
  styleUrls: ['./import-csv.component.css']
})
export class ImportCsvComponent implements OnInit {

  private readonly orderClient: OrderClientService;
  private procesedResults: Map<string, number> = new Map();

  constructor(
    orderClient: OrderClientService,
    private router: Router,
    private globalService: GlobalService
    ) 
  {
    this.orderClient = orderClient;
  }

  ngOnInit() 
  {
  }

  get errorOrders(): number[]
  {
    let result = [];
    this.procesedResults.forEach((v, k)=>
    { 
      if(v==-1)
      {
        result.push(k);
      }

    });
    return result;
  }

  get okOrdersCount(): number
  {
    let counter=0;
    this.procesedResults.forEach((v) =>
    {
      if(v==0) counter++;
    })
    return counter;
  }

  get errorOrdersCount(): number
  {
    let counter=0;
    this.procesedResults.forEach((v, k) =>
    {
      if(v==-1) counter++;
    })
    return counter;
  }

  onCsvSelected(event: Event)
  {
    //capture the element that raise event
    let target = event.target as HTMLInputElement;

    let file = target.files[0];
    if(file)
    {
      let fileReader = new FileReader();
      
      fileReader.readAsText(file, "UTF-8");
      fileReader.onload = ()=>
      {
        let fileContent = fileReader.result as string;
        let lines = fileContent.split(/\r\n|\n/);
        //we dont need the header
        lines.splice(0,1);
        // delete empty lines
        let fileLines= lines.filter((value) => value !== '');
        // parser file content
        let ordersArray = this.parserFileData(fileLines);
        if(ordersArray && 0 < ordersArray.length)
        {
          ordersArray.forEach(order => {
                    //patch for dateRequired
              order.required = "12-08-2019";
              this.orderClient.addNewOrder(order).subscribe(
                (res: number)=>
                {
                  // 0 : ok
                  //-1 error
                  this.procesedResults.set(order.number, res);
                },
                (error)=>
                {
                  console.log(error);
                }
              );
          });
        }
      }
    }

  }

  parserFileData(orderData: string[]): Order[]
  {
    let result: Order[] = [];
    orderData.forEach(line => {
      //Example
      //GOLF-0001,LED Lamp,"GLL-067-R3,1,GLL-011,6,GLL-023,1,GLL-024-1,1"
      let splitedLine = line.split(',');
      let orderNumber = splitedLine[0];
      let orderDescription = splitedLine[1];
      let orderComponents : ComponentOrder[] = [];

      
      for(var i= 2; i < splitedLine.length/2; i+=2)
      {
       
        let currentComponent = new ComponentOrder();
        currentComponent.componentCode = splitedLine[i];
        currentComponent.componentQuantity = Number.parseInt( splitedLine[i+1]);
        orderComponents.push(currentComponent);
      
      }

      let orderToImport = new Order();
      orderToImport.number = orderNumber;
      orderToImport.components = orderComponents;
      //orderToImport.required
      orderToImport.required = "12-08-2019";
      orderToImport.description = orderDescription;

      //get user
      let localStorageData = this.globalService.loginData; 
      let userData: LoginResponse = JSON.parse(localStorageData) ;

      orderToImport.createdBy = new User(); 

      orderToImport.createdBy.guid = userData.guid;

      result.push(orderToImport);

    });
    return result;
  }

  backNavigation()
  {
    this.router.navigate(['orders']);
  }
}
