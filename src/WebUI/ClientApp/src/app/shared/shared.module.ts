import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CdkTableModule } from '@angular/cdk/table';
import { MaterialModule } from './material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { RouterModule } from '@angular/router';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { SplitStringPipe } from './services/splitstring.service';
import { EnumpipeService } from './services/enumpipe.service';
import { CovalentModule } from './covalent.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        CdkTableModule,
        AngularEditorModule,
        MaterialModule,
        CovalentModule,
        RouterModule,
        FlexLayoutModule
    ],
    declarations: [
        EnumpipeService,
        SplitStringPipe,
    ],
    exports: [
        CovalentModule,
        CommonModule,
        AngularEditorModule,
        FlexLayoutModule,
        EnumpipeService,
        SplitStringPipe,
        FormsModule,
        ReactiveFormsModule,
        MaterialModule,
    ],
    providers: [],
    entryComponents: [
    ],
})
export class SharedModule { }
