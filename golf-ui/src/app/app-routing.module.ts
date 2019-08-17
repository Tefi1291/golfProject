import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './helper/AuthGuard';
import { OrderListComponent } from './order-list/order-list.component';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { ImportCsvComponent } from './import-csv/import-csv.component';

const routes: Routes = 
[
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'login', component: LoginComponent },
   {path:'orders', component: OrderListComponent, canActivate: [AuthGuard]},
  {path: 'orders/add', component: OrderDetailsComponent, canActivate: [AuthGuard]},
  {path:'orders/:orderId', component: OrderDetailsComponent, canActivate:  [AuthGuard] },
  {path:'import', component: ImportCsvComponent, canActivate:[AuthGuard]}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule 
{
  

}
