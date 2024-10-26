import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseEndpoint } from '../base-entity/base.endpoint';
import { Truck } from './truck.models';

@Injectable({
    providedIn: 'root'
})
export class TruckEndpoint extends BaseEndpoint<Truck> {
    override get endpoint(): string {
        return '/odata/Truck'
    }

    constructor(override httpClient: HttpClient) {
        super(httpClient);
    }
}