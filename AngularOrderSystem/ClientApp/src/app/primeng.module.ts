import { NgModule } from '@angular/core';

import { ButtonModule, GrowlModule, ScheduleModule, InputTextModule, DropdownModule, CalendarModule, PanelModule } from 'primeng/primeng';

@NgModule({
    imports: [
        ButtonModule,
        GrowlModule,
        ScheduleModule,
        InputTextModule,
        DropdownModule,
        CalendarModule,
        PanelModule
    ],
    exports: [
        ButtonModule,
        GrowlModule,
        ScheduleModule,
        InputTextModule,
        DropdownModule,
        CalendarModule,
        PanelModule
    ]
})
export class PrimeNgModule { }
