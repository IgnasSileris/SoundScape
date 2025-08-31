import {
  IconDashboard,
  IconHelp,
  IconInfoCircle,
  IconMicrophone,
  IconSettings,
  IconUser
} from '@tabler/icons-react'
import NavElement from './NavElement'
import {
  accountRoute,
  dashboardRoute,
  helpRoute,
  micAndFiltersRoute,
  settingsRoute,
  testAndCompareRoute
} from '../app/router'

export default function NavBar() {
  const navSections = [
    {
      name: 'Dashboard',
      icon: IconDashboard,
      route: dashboardRoute
    },
    {
      name: 'Mic & Filters',
      icon: IconMicrophone,
      route: micAndFiltersRoute
    },
    {
      name: 'Test & Compare',
      icon: IconInfoCircle,
      route: testAndCompareRoute
    },
    { name: 'Account', icon: IconUser, route: accountRoute },
    { name: 'Settings', icon: IconSettings, route: settingsRoute },
    { name: 'Help', icon: IconHelp, route: helpRoute }
  ]

  return (
    <div className="flex h-full w-full justify-center bg-slate-900">
      <div className="mt-[40%] flex w-5/6 flex-col items-stretch">
        {navSections.map((section) => (
          <NavElement
            key={section.name}
            title={section.name}
            route={section.route}
            Icon={section.icon}
          />
        ))}
      </div>
    </div>
  )
}
