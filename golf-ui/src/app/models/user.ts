import { Injectable } from '@angular/core';

@Injectable()
export class User
{
    id:number;
    firstname: string;
    lastname: string;
    username: string;
    guid:string;
}