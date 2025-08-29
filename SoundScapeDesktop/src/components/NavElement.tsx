import { Button } from './ui/button'

export default function NavElement({
  title,
  Icon
}: {
  title: string
  Icon: React.ElementType
}) {
  return (
    <div>
      <Button className="flex h-16 w-full items-center justify-start gap-5 rounded-xl bg-transparent font-sans text-lg font-light shadow-none hover:bg-sky-300/5 hover:text-sky-300">
        <Icon style={{ width: '22px', height: '22px' }} />
        <span>{title}</span>
      </Button>
    </div>
  )
}
