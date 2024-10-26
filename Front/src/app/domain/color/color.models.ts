import { BaseEntity } from "../base-entity/base-entity.model";
import { Truck } from "../truck/truck.models";

export class Color extends BaseEntity {
    public Modified?: Date | null = null;
    public Name: string = '';
    public SapCode: string = '';
    public HexaColor: string = '';
    public Truck: Truck[] | null = [];
}
