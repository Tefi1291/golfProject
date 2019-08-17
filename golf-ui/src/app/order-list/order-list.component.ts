import { Component, OnInit } from '@angular/core';
import { Order } from '../models/order';
import { OrderClientService } from '../services/orderClient.service';
import { Router, NavigationExtras } from '@angular/router';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit {

  private listOrders: Order[] = [];

  constructor(
    private _client: OrderClientService,
    private _router: Router)
  { }

  ngOnInit() {
    
    //get order list
    this._client.getOrders().subscribe(
      (data: Order[]) =>
      {
        //debugging  
        console.log(JSON.stringify(data));

        for(let order of data)
        {
          if(!order.components)
          {
            order.components = [];
          }
          this.listOrders.push(order);
        }
        this.listOrders.sort(this.sortOrdersByDate);
      },
      (error: Error)=>
      {
        console.log(error);
      }
    );
  }

  sortOrdersByDate(ordera: Order, orderb: Order): number
  {
    let dateArrayA = ordera.required.split('-');
    let dateArrayB = orderb.required.split('-');
    let dateA = Date.parse(`${dateArrayA[2]}-${dateArrayA[1]}-${dateArrayA[0]}`);

    var dateB = Date.parse(`${dateArrayB[2]}-${dateArrayB[1]}-${dateArrayB[0]}`);
    return  (dateA<dateB ? -1 : 1);
  }

  orderDetails(order:Order): void
  {
    this._client.selectedOrder = order;
    this._router.navigate(['orders/' + order.id]);

  }

  addOrderNavigate(): void
  {

    this._client.selectedOrder = new Order();
    this._router.navigate(['orders/add']);
  }

  importCsvNavigate(): void
  {
    this._router.navigate(['import']);
  }
}
