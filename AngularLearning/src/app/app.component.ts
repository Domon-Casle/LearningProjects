import { Component, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource, MatSort } from '@angular/material';

export interface PeopleElement {
  name: string;
  symbol: string;
}

const PEOPLE_DATA: PeopleElement[] = [
  { name: "Bill", symbol: "B" },
  { name: "Phil", symbol: "P" },
  { name: "Chow", symbol: "C" },
  { name: "Bob",  symbol: "B" },
  { name: "Josh", symbol: "J" },
  { name: "Bill2", symbol: "B" },
  { name: "Phil2", symbol: "P" },
  { name: "Chow2", symbol: "C" },
  { name: "Bob2",  symbol: "B" },
  { name: "Josh2", symbol: "J" },
  { name: "Bill3", symbol: "B" },
  { name: "Phil3", symbol: "P" },
  { name: "Chow3", symbol: "C" },
  { name: "Bob3",  symbol: "B" },
  { name: "Josh3", symbol: "J" },
];

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  dataSource = new MatTableDataSource<PeopleElement>(PEOPLE_DATA);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    this.dataSource.filterPredicate = 
      (data: PeopleElement, filter: string) => data.name.toLowerCase().startsWith(filter);
  }

  title = 'AngularLearning';
  favorites = [
    {
      value: 1,
      viewValue: "Gem"
    },
    {
      value: 2,
      viewValue: "Red"
    }
  ];

  displayedColumns = [
    'name', 'symbol'
  ];

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
