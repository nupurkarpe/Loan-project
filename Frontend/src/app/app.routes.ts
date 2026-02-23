import { Routes } from '@angular/router';
import { Navigation } from './layout/navigation/navigation';
import { Login } from './auth/login/login';
import { Signup } from './auth/signup/signup';

// LOS
import { Customer } from './los/customer/customer';
import { Application as LOSApplication } from './los/application/application';
import { Eligibility } from './los/eligibility/eligibility';
import { CreditEvaluation } from './los/credit-evaluation/credit-evaluation';
import { SanctionDisbursement } from './los/sanction-disbursement/sanction-disbursement';

// LMS
import { Dashboard as LMSDashboard } from './lms/dashboard/dashboard';
import { EmiSchedule } from './lms/emi-schedule/emi-schedule';
import { Payment } from './lms/payment/payment';
import { Closure } from './lms/closure/closure';

export const routes: Routes = [
    { path: 'login', component: Login },
    { path: 'signup', component: Signup },
    {
        path: '',
        component: Navigation,
        children: [
            // LOS Flow Routes
            { path: 'los/customer', component: Customer },
            { path: 'los/application', component: LOSApplication },
            { path: 'los/eligibility', component: Eligibility },
            { path: 'los/credit-evaluation', component: CreditEvaluation },
            { path: 'los/sanction-disbursement', component: SanctionDisbursement },

            // LMS Dashboard Routes
            { path: 'lms/dashboard', component: LMSDashboard },
            { path: 'lms/emi-schedule', component: EmiSchedule },
            { path: 'lms/payment', component: Payment },
            { path: 'lms/closure', component: Closure },

            // Default Redirects
            { path: '', redirectTo: 'los/customer', pathMatch: 'full' }
        ]
    },
    { path: '**', redirectTo: 'login' }
];
