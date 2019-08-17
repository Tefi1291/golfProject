import { Injectable } from '@angular/core';
import { User } from './user';
import { ComponentOrder } from './component';

@Injectable()
export class Order
{
    id: number;
    number: string;
    created: string;
    required: string;
    description: string;
    createdBy: User;
    components: ComponentOrder[];

    constructor()
    {
        this.number = "";
        this.created = "";
        this.required = "";
        this.description = "";
        this.createdBy = new User();
        this.components = new Array<ComponentOrder>();
    }

}