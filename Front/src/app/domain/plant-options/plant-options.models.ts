import { BaseEntity } from "../base-entity/base-entity.model";
import { Truck } from "../truck/truck.models";

export class PlantOptions extends BaseEntity {
    public Modified?: Date | null = null;
    public Key?: string | null = null;
    public DisplayName: string = '';
    public Truck: Truck[] | null = [];
}
