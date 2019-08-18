import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../models/order';
import { GlobalService } from '../global.service';
import { LoginResponse } from '../apiModels/login-response';

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
  private _token: string;

  constructor(
    private client: HttpClient,
    private globalData: GlobalService
    )
  { 
    var loginResponse = this.globalData.loginData;
    if(loginResponse)
    {
      var loginData = JSON.parse(loginResponse) as LoginResponse;
      this._token = loginData.token;
      console.log( loginData.token);
      
    }
  }

  getAuthHeader():HttpHeaders
  {
    return new HttpHeaders().append('authorization', 'Bearer ' + this._token)
  }

  getOrders(): Observable<object> {
    console.log("Request Api: " + this._orderEndpoint);
    
    return this.client.get(this._orderEndpoint, {headers: this.getAuthHeader() });
  }

  getOrderDetails(orderId: number): Observable<Object>
  {
    let endpoint = `${this._orderEndpoint}${orderId}` ;
    console.log("Request Api: " + endpoint);

    return this.client.get(endpoint,
      {headers: this.getAuthHeader() }
      );
  }

  addNewOrder(order: Order): Observable<Object>
  {
    return this.client.post(this._orderEndpoint, 
      order,
      {headers: this.getAuthHeader() }
      );
  }

  updateOrder(order: Order):Observable<object>
  {
    var id = order.id; 
    return this.client.put(
      `${this._orderEndpoint}${id.toString()}`,
      order,
      {headers: this.getAuthHeader() }
    );
  }

}
