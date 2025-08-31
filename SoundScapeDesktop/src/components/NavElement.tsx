import { cn } from '../lib/utils'
import { Button } from './ui/button'
import { AnyRoute, useMatchRoute, useNavigate } from '@tanstack/react-router'

export default function NavElement({
  title,
  Icon,
  route
}: {
  title: string
  Icon: React.ElementType
  route: AnyRoute
}) {
  const navigate = useNavigate()
  const match = useMatchRoute()

  const isActive = !!match({ to: route.path })

  const buttonStyleClass = cn(
    'flex h-16 w-full items-center justify-start gap-5 rounded-xl bg-transparent font-sans text-lg font-light shadow-none hover:bg-sky-300/5 hover:text-sky-300',
    isActive && 'bg-sky-200/5 text-sky-200'
  )

  return (
    <div>
      <Button
        onClick={() => navigate({ from: '/', to: route.path })}
        className={buttonStyleClass}
      >
        <Icon style={{ width: '22px', height: '22px' }} />
        <span>{title}</span>
      </Button>
    </div>
  )
}
