import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {ReactiveFormsModule, FormsModule} from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { LoginComponent } from './login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Login } from './services/login.service';
import {HttpClientModule} from '@angular/common/http';
import { AlertDialogComponent } from './alert-dialog/alert-dialog.component';
import { AuthGuard } from './helper/AuthGuard';
import { OrderListComponent } from './order-list/order-list.component';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { OrderClientService } from './services/orderClient.service';
import { ImportCsvComponent } from './import-csv/import-csv.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    SidebarComponent,
    LoginComponent,
    
    OrderListComponent,
    OrderDetailsComponent,
    ImportCsvComponent,
    
    // AlertDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    
  ],
  providers: 
  [
    Login,
    AuthGuard,
    OrderClientService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
