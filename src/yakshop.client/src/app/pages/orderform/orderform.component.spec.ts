import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { StockInfo } from '../../models/StockInfo';
import { OrderFormComponent } from './orderform.component';

describe('OrderformComponent', () => {
  let component: OrderFormComponent;
  let fixture: ComponentFixture<OrderFormComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    TestBed.configureTestingModule({
      declarations: [OrderFormComponent],
      imports: [HttpClientTestingModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve stock information from the server', () => {
    const mockStockInfo: StockInfo = { dayNumber: 1, milk: 99.67, skins: 3 };

    component.ngOnInit();

    const req = httpMock.expectOne('/stockinfo');
    expect(req.request.method).toEqual('GET');
    req.flush(mockStockInfo);

    expect(component.stockInfo).toEqual(mockStockInfo);
  });
});
