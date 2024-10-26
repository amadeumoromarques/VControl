import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseEndpoint } from '../base-entity/base.endpoint';
import { TruckType } from './truck-type.models';

@Injectable({
    providedIn: 'root'
})
export class TruckTypeEndpoint extends BaseEndpoint<TruckType> {
    override get endpoint(): string {
        return '/odata/TruckType'
    }

    constructor(override httpClient: HttpClient) {
        super(httpClient);
    }
}