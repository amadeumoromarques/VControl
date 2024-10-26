import { BaseEntity } from "../base-entity/base-entity.model";
import { Color } from "../color/color.models";
import { PlantOptions } from "../plant-options/plant-options.models";
import { TruckType } from "../truck-type/truck-type.models";

export class Truck extends BaseEntity {
    public Modified?: Date | null = null;
    public ChassisCode: string = '';
    public ManufacturerYear: number = 0;
    public IdTruckType: number = 0;
    public IdPlantOptions: number = 0;
    public IdColor: number = 0;
    public TruckType: TruckType = new TruckType();
    public PlantOptions: PlantOptions = new PlantOptions();
    public Color: Color = new Color();
}