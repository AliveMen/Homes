import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Building} from '../shared/building';
import {ISearchCriteria} from '../shared/interfaces';

@Injectable({providedIn: 'root'})
export class BuildingService {
  constructor(private http: HttpClient) {
  }

  save(building: Building): Observable<Building> {
    return this.http.post<Building>(`/buildings`, building);
  }

  getById(id: string): Observable<Building> {
    return this.http.get<Building>(`/buildings/${id}`);
  }

  remove(id: string): Observable<void> {
    return this.http.delete<void>(`/buildings`);
  }

  search(searchCriteria: ISearchCriteria): Observable<Building[]> {
    console.log(searchCriteria);
    return this.http.post<Building[]>(`/buildings/search`, searchCriteria);
  }
}
