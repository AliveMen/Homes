import {Building} from './building';
import {ITenant} from './interfaces';

export class Tenant implements ITenant {
  id!: string;
  name!: string;
  endRentDate!: Date;
  buildingId?: string | undefined;
  building?: Building;

  constructor(data?: ITenant) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  init(_data?: any) {
    if (_data) {
      this.id = _data["id"];
      this.name = _data["name"];
      this.endRentDate = _data["endRentDate"] ? new Date(_data["endRentDate"].toString()) : <any>undefined;
      this.buildingId = _data["buildingId"];
      this.building = _data["building"] ? Building.fromJS(_data["building"]) : <any>undefined;
    }
  }

  static fromJS(data: any): Tenant {
    data = typeof data === 'object' ? data : {};
    let result = new Tenant();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data["id"] = this.id;
    data["name"] = this.name;
    data["endRentDate"] = this.endRentDate ? this.endRentDate.toISOString() : <any>undefined;
    data["buildingId"] = this.buildingId;
    data["building"] = this.building ? this.building.toJSON() : <any>undefined;
    return data;
  }
}
