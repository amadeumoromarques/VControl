import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseEndpoint } from '../base-entity/base.endpoint';
import { PlantOptions } from './plant-options.models';

@Injectable({
    providedIn: 'root'
})
export class PlantOptionsEndpoint extends BaseEndpoint<PlantOptions> {
    override get endpoint(): string {
        return '/odata/PlantOptions'
    }

    constructor(override httpClient: HttpClient) {
        super(httpClient);
    }
}