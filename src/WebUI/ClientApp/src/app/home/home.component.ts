import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {
  AdvertisementClient, CreateAdvertisementCommand,
  GetAdvertisementsWithPaginationQuery,
} from '../web-api-client';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  constructor(
    private advertisementClient: AdvertisementClient,
    private changeDetectorRef: ChangeDetectorRef,
    private fb: FormBuilder
  ) {
    this.addImage();
  }

  public paginatedListOfTodoItemBriefDto!: any;
  public filterForm = new FormGroup({
    orderBy: new FormControl('0'),
    orderType: new FormControl('0'),
    pageSize: new FormControl(10),
  });
  public createAdForm = new FormGroup({
    label: new FormControl(''),
    amount: new FormControl('0'),
    description: new FormControl(''),
    images: this.fb.array([])
  });

  ngOnInit(): void {
    this.getAdvertisementsWithPagination(1);
  }

  public getAdvertisementsWithPagination(pageNumber: number) {
    this.paginatedListOfTodoItemBriefDto = undefined;
    // tslint:disable-next-line:prefer-const
    let query = new GetAdvertisementsWithPaginationQuery();
    query.pageNumber = pageNumber;
    query.pageSize = this.filterForm.get('pageSize').value;
    query.orderBy = Number(this.filterForm.get('orderBy').value);
    query.orderType = Number(this.filterForm.get('orderType').value);
    this.advertisementClient.getAdvertisementsWithPagination(query).subscribe( x => {
      this.paginatedListOfTodoItemBriefDto = x;
      this.changeDetectorRef.detectChanges();
    });
  }

  // private create(query: CreateAdvertisementCommand) {
  //   console.log(this.advertisementClient.create(query).subscribe());
  // }

  get images(): FormArray {
    return this.createAdForm.controls['images'] as FormArray;
  }

  addImage() {
    const imageForm = this.fb.group({
      url: ['', Validators.required],
    });

    this.images.push(imageForm);
  }

  deleteImage(lessonIndex: number) {
    this.images.removeAt(lessonIndex);
  }

  createAd() {
    const command = new CreateAdvertisementCommand();
    command.title = this.createAdForm.get('label').value;
    command.amount = this.createAdForm.get('amount').value;
    command.description = this.createAdForm.get('description').value;
    command.imagesUrl = this.createAdForm.get('images').value;
    console.log(command);
    this.advertisementClient.createAd(command).subscribe((response) => {});
    console.log(this.createAdForm);
  }
}
