import {
  IconDashboard,
  IconHelp,
  IconInfoCircle,
  IconMicrophone,
  IconSettings,
  IconUser
} from '@tabler/icons-react'
import NavElement from './NavElement'

export default function NavBar() {
  const navSections = [
    { name: 'Dashboard', icon: IconDashboard },
    { name: 'Mic & Filters', icon: IconMicrophone },
    { name: 'Test & Compare', icon: IconInfoCircle },
    { name: 'Account', icon: IconUser },
    { name: 'Settings', icon: IconSettings },
    { name: 'Help', icon: IconHelp }
  ]

  return (
    <div className="flex h-full w-full justify-center bg-slate-900">
      <div className="mt-[40%] flex w-5/6 flex-col items-stretch">
        {navSections.map((section) => (
          <NavElement
            key={section.name}
            title={section.name}
            Icon={section.icon}
          />
        ))}
      </div>
    </div>
  )
}
