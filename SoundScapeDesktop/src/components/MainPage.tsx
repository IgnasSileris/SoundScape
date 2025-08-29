import ContentDisplay from './ContentDisplay'
import NavBar from './NavBar'

export default function MainPage() {
  return (
    <div className="flex h-screen w-screen">
      <div className="h-full w-72 min-w-72 border border-red-500">
        <NavBar />
      </div>
      <div className="h-full w-full border border-red-500">
        <ContentDisplay />
      </div>
    </div>
  )
}
