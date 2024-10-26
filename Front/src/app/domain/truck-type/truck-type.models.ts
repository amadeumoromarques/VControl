import { BaseEntity } from "../base-entity/base-entity.model";
import { Truck } from "../truck/truck.models";

export class TruckType extends BaseEntity {
    public Modified?: Date | null = null;
    public Type: string = '';
    public Truck: Truck[] | null = [];
}
