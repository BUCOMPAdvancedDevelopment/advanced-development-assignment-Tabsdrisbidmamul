import { DOCUMENT } from '@angular/common';
import {
  AfterViewInit,
  Component,
  HostListener,
  Inject,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import {
  base64ToFile,
  ImageCroppedEvent,
  ImageTransform,
  LoadedImage,
} from 'ngx-image-cropper';
import { of, Subject, takeUntil } from 'rxjs';
import { IUserDTO } from 'src/app/interfaces/user.interface';
import { AuthService } from 'src/app/services/http/auth/auth.service';
import { ProfileEditService } from 'src/app/services/http/profile/profile-edit.service';

@Component({
  selector: 'app-profile-image-edit',
  templateUrl: './profile-image-edit.component.html',
  styleUrls: ['./profile-image-edit.component.scss'],
})
export class ProfileImageEditComponent
  implements OnInit, OnDestroy, AfterViewInit
{
  private destroy$ = new Subject<void>();
  user: IUserDTO | null = null;

  imageChangedEvent: any = '';
  croppedImage: any = '';

  fileUploader!: HTMLInputElement;

  profileImageForm = new FormGroup({
    File: new FormControl<Blob>(new Blob()),
    Path: new FormControl(''),
  });

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private _authService: AuthService,
    private _profileEditService: ProfileEditService
  ) {}

  ngOnInit(): void {
    this._authService.user$.pipe(takeUntil(this.destroy$)).subscribe((user) => {
      if (user !== null) {
        this.user = user;
      }
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  ngAfterViewInit(): void {
    this.fileUploader = this.document.getElementById(
      'profile-image-uploader'
    ) as HTMLInputElement;
  }

  handleOnDelete() {
    this._profileEditService
      .deleteImage()
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        if (this.user !== null) {
          this.user.image = null;

          this._authService.user$.next(this.user);
        }
      });
  }

  handleOnUpload() {
    (
      this.document.getElementById('profile-image-uploader') as HTMLInputElement
    ).click();
  }

  handleOnSaveImage() {
    const formData = new FormData();
    formData.append('File', base64ToFile(this.croppedImage));
    formData.append('Path', 'logo/profile-images');

    this.profileImageForm.controls.File.setValue(
      base64ToFile(this.croppedImage)
    );

    this.profileImageForm.controls.Path.setValue('logo/profile-images');

    this._profileEditService
      .addImage(formData)
      .pipe(takeUntil(this.destroy$))
      .subscribe((image) => {
        if (this.user !== null) {
          this.user.image = image;

          this._authService.user$.next(this.user);
        }
      });
  }

  handleOnCancel() {
    this.imageChangedEvent = null;

    this.fileUploader.value = '';
  }

  fileChangeEvent(event: any): void {
    this.imageChangedEvent = event;
  }

  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
    console.log(event, base64ToFile(event.base64!));
  }

  imageLoaded(image?: LoadedImage) {
    // show cropper
  }

  cropperReady() {
    // cropper ready
  }

  loadImageFailed() {
    // show message
  }
}
