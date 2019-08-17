import { Component, OnInit, Input } from '@angular/core';
import { Router, Route, ActivatedRoute } from '@angular/router';
import { OrderClientService } from '../services/orderClient.service';
import { Order } from '../models/order';
import { ComponentOrder } from '../models/component'
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { forEach } from '@angular/router/src/utils/collection';
import { User } from '../models/user';
import { post } from 'selenium-webdriver/http';
import { GlobalService } from '../global.service';
import { LoginResponse } from '../apiModels/login-response';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {
  @Input() orderId: number;
  order: Order;

  constructor(
    private _router: Router,
    private _activatedRoute: ActivatedRoute,
    private _orderClient: OrderClientService,
    private _globalService: GlobalService
  ) { }

  ngOnInit() {
    this._activatedRoute.params.subscribe((params) => {
      this.orderId = +params['orderId']
    });
    this.order = this._orderClient.selectedOrder;

    // if not order selected -> get from server
    if (!this.order && this.orderId) {
      this._orderClient.getOrderDetails(this.orderId).subscribe(
        (data: Order) => {
          this.order = data;
        });
    }
  }


  deleteComponent(index: number) {
    this.order.components.splice(index, 1);

  }

  addNewComponent() {
    this.order.components.push(new ComponentOrder());

  }

  navigateBack() {
    this._router.navigate(['orders']);
  }

  submitForm() {
    console.log("submiting form...");
    //getting ready user
    let localStorageData = this._globalService.loginData ;
    let userData: LoginResponse = JSON.parse(localStorageData); 

    this.order.createdBy = new User();
    this.order.createdBy.guid = userData.guid;

    //if is a new order
    //post
    if (this.isAddOrder) {
      this.postNewOrder(this.order);
    }
    else {
      //else, update current order details
      this.updateOrder(this.order);

    }

  }

  get isAddOrder(): boolean {
    return this._activatedRoute.routeConfig.path == "orders/add";
  }

  postNewOrder(order: Order) {
    this._orderClient.addNewOrder(order).subscribe(
      (res) => {
        //check codes
        this.navigateBack();
      },
      (error) => {
        console.log(error);
      }

    );
  }

  updateOrder(order: Order) {
    this._orderClient.updateOrder(order).subscribe(
      (res) => {
        this.navigateBack();
      },
      (error) => {
        this.navigateBack();
      }
    )

  }

  logger() {
    return JSON.stringify(this.order);

  }

}
