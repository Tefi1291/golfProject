import { Injectable } from '@angular/core';

@Injectable()
export class ComponentOrder
{
    id: number;
    componentCode: string;
    componentQuantity: number;
    constructor()
    {
        this.componentCode = "";
        this.componentQuantity = 0;
    }

}