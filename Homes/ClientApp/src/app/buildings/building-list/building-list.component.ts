import { Component, OnInit } from '@angular/core';
import {IBuilding} from '../../shared/interfaces';
import {BuildingService} from '../../services/api.service';

@Component({
  selector: 'app-building-list',
  templateUrl: './building-list.component.html',
  styleUrls: ['./building-list.component.css']
})
export class BuildingListComponent implements OnInit {
  buildings: IBuilding[];

  constructor(private buildingService: BuildingService) { }

  ngOnInit() {

  }

}
