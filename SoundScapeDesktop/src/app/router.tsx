import {
  createRootRoute,
  createRoute,
  createRouter
} from '@tanstack/react-router'
import MainPage from '../pages/MainPage'
import DashboardPage from '../pages/DashboardPage'
import MicAndFiltersPage from '../pages/MicAndFiltersPage'
import TestAndComparePage from '../pages/TestAndComparePage'
import AccountPage from '../pages/AccountPage'
import SettingsPage from '../pages/SettingsPage'
import HelpPage from '../pages/HelpPage'

const rootRoute = createRootRoute({
  component: MainPage
})

const indexRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/',
  component: DashboardPage
})

export const dashboardRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/dashboard',
  component: DashboardPage
})

export const micAndFiltersRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/micfilters',
  component: MicAndFiltersPage
})

export const testAndCompareRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/testcompare',
  component: TestAndComparePage
})

export const accountRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/account',
  component: AccountPage
})

export const settingsRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/settings',
  component: SettingsPage
})

export const helpRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/help',
  component: HelpPage
})

const routeTree = rootRoute.addChildren([
  indexRoute,
  dashboardRoute,
  micAndFiltersRoute,
  testAndCompareRoute,
  accountRoute,
  settingsRoute,
  helpRoute
])

export const router = createRouter({ routeTree })

declare module '@tanstack/react-router' {
  interface Register {
    router: typeof router
  }
}
