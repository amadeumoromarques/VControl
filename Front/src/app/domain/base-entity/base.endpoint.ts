import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseEntity } from './base-entity.model';
import { ODataListResult, ODataQueryCommand } from '@gmz/ngx-b-toolkit/odata';
import { isDefaultIdValue, RestCommand } from '@gmz/ngx-b-toolkit/common';
import { environment } from '../../../environments/environment';

export abstract class BaseEndpoint<TEntity extends BaseEntity> {

    abstract get endpoint(): string;

    constructor(public httpClient: HttpClient) { }

    getAll(queryString?: string): Observable<ODataListResult<TEntity>> {
        return this.httpClient.get<ODataListResult<TEntity>>(this.buildEndpoint({ url: [''], queryString: queryString }));
    }

    getById(id: string, queryString?: string): Observable<TEntity> {
        return this.httpClient.get<TEntity>(this.buildEndpoint({ url: ['/', id], queryString: queryString }));
    }

    save(param: TEntity, queryString?: string): Observable<TEntity> {
        return this.httpClient.post<TEntity>(this.buildEndpoint({ url: [''], queryString: queryString }), param);
    }

    delete(id: string): Observable<TEntity> {
        return this.httpClient.delete<TEntity>(this.buildEndpoint({ url: ['/', id], }));
    }

    getByIdCommand(entityId : () => string) : RestCommand {
        return new RestCommand((command : RestCommand) => {
            return this.getById(entityId());
        });
    }

    getAllCommand() : ODataQueryCommand<TEntity> {
        return new ODataQueryCommand((command : ODataQueryCommand) => {
            return this.getAll(command.state.queryString.build());
        });
    }

    saveCommand(bodyFunc : () => TEntity) : RestCommand {
        return new RestCommand((command : RestCommand) => {
            return this.save(bodyFunc());
        })
    }

    deleteCommand(entityId : () => string) : RestCommand {
        return new RestCommand((command : RestCommand) => {
            return this.delete(entityId());
        })
    }

    public buildEndpoint({ url = [], queryString }: { url?: any[], queryString?: string }): string {
        url.unshift(this.endpoint);

        return buildEndpoint(url, queryString);
    }
}

export function buildEndpoint(url: string[] = [], queryString?: string): string {
    if (queryString) {
        if (!queryString.startsWith("?")) {
            queryString = `?${queryString}`;
        }
    }
    else {
        queryString = "";
    }

    return environment.apiEndpoint + url.join("") + queryString;
}
