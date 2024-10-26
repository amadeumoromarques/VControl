import { Routes } from '@angular/router';
import { TruckListPage } from './features/truck/pages/truck-list/truck-list.page';
import { TruckTypeListPage } from './features/truck-type/pages/truck-type-list/truck-type-list.page';
import { PlantListPage } from './features/plant/pages/plant-list/plant-list.page';
import { ColorListPage } from './features/color/pages/color-list/color-list.page';

export const routes: Routes = [
    {
        path: 'truck',
        children: [
            {
                path: 'list',
                component: TruckListPage
            }
        ]
    },
    {
        path: 'truck-type',
        component: TruckTypeListPage,
        children: [
            {
                path: 'list',
                component: TruckTypeListPage
            }
        ]
    },
    {
        path: 'plant-options',
        component: PlantListPage,
        children: [
            {
                path: 'list',
                component: PlantListPage
            }
        ]
    },
    {
        path: 'color',
        component: ColorListPage,
        children: [
            {
                path: 'list',
                component: ColorListPage
            }
        ]
    },
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'truck/list'
    },
    {
        path: '**',
        redirectTo: 'truck/list'
    }
];


