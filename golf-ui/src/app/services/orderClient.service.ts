import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderClientService {
  
  private _orderEndpoint = "http:///localhost:5000/api/orders/";
  private _selectedOrder: Order;
  get selectedOrder():Order
  {
    return this._selectedOrder;
  }
  set selectedOrder(newOrder: Order)
  {
    this._selectedOrder = newOrder;
  }


  constructor(
    private client: HttpClient
    )
  { }

  getOrders(): Observable<object> {
    console.log("Request Api: " + this._orderEndpoint);
    return this.client.get(this._orderEndpoint);
  }

  getOrderDetails(orderId: number): Observable<Object>
  {
    let endpoint = `${this._orderEndpoint}${orderId}` ;
    console.log("Request Api: " + endpoint);

    return this.client.get(endpoint);
  }

  addNewOrder(order: Order): Observable<Object>
  {
    return this.client.post(this._orderEndpoint, order);
  }

  updateOrder(order: Order):Observable<object>
  {
    var id = order.id; 
    return this.client.put(
      `${this._orderEndpoint}${id.toString()}`,
      order
    );
  }

}
