import { Outlet } from '@tanstack/react-router'
import NavBar from '../components/NavBar'

export default function MainPage() {
  return (
    <div className="flex h-screen w-screen">
      <div className="h-full w-72 min-w-72 border border-red-500">
        <NavBar />
      </div>
      <div className="h-full w-full border border-red-500 bg-slate-950/95">
        {' '}
        <Outlet />
      </div>
    </div>
  )
}
