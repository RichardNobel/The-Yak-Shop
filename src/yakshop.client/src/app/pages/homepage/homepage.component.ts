import { Component, OnInit } from '@angular/core';

import * as initjs from '../../../assets/js/ecommerce';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    initjs.initSwiper();
  }

}
