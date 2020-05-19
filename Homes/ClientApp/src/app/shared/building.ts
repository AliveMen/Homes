import {IBuilding} from './interfaces';
import {Tenant} from './tenant';

export class Building implements IBuilding {
  id!: string;
  name?: string | undefined;
  levels!: number;
  foundationYear!: number;
  city!: string;
  postCode!: number;
  addressLine!: string;
  image?: string | undefined;
  tenants?: Tenant[] | undefined;

  constructor(data?: IBuilding) {
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
      this.levels = _data["levels"];
      this.foundationYear = _data["foundationYear"];
      this.city = _data["city"];
      this.postCode = _data["postCode"];
      this.addressLine = _data["addressLine"];
      this.image = _data["image"];
      if (Array.isArray(_data["tenants"])) {
        this.tenants = [] as any;
        for (let item of _data["tenants"])
          this.tenants!.push(Tenant.fromJS(item));
      }
    }
  }

  static fromJS(data: any): Building {
    data = typeof data === 'object' ? data : {};
    let result = new Building();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data["id"] = this.id;
    data["name"] = this.name;
    data["levels"] = this.levels;
    data["foundationYear"] = this.foundationYear;
    data["city"] = this.city;
    data["postCode"] = this.postCode;
    data["addressLine"] = this.addressLine;
    data["image"] = this.image;
    if (Array.isArray(this.tenants)) {
      data["tenants"] = [];
      for (let item of this.tenants)
        data["tenants"].push(item.toJSON());
    }
    return data;
  }
}
