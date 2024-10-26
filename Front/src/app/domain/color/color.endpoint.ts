import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseEndpoint } from '../base-entity/base.endpoint';
import { Color } from './color.models';

@Injectable({
    providedIn: 'root'
})
export class ColorEndpoint extends BaseEndpoint<Color> {
    override get endpoint(): string {
        return '/odata/Color'
    }

    constructor(override httpClient: HttpClient) {
        super(httpClient);
    }
}