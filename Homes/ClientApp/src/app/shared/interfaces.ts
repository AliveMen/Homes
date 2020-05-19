import {Tenant} from './tenant';
import {Building} from './building';

export interface ITenant {
  id: string;
  name: string;
  endRentDate: Date;
  buildingId?: string | undefined;
  building?: Building;
}

export interface IBuilding {
  id: string;
  name?: string | undefined;
  levels: number;
  foundationYear: number;
  city: string;
  postCode: number;
  addressLine: string;
  image?: string | undefined;
  tenants?: Tenant[] | undefined;
}

export interface ISearchCriteria {
  objectIds?: string[] | undefined;
  skip?: number;
  take?: number;
  keyword?: string | undefined;
}

export interface ITenantSearchCriteria {
  buildingId?: string | undefined;
  objectIds?: string[] | undefined;
  skip?: number;
  take?: number;
  keyword?: string | undefined;
}

