import {
  Select,
  SelectValue,
  SelectTrigger,
  SelectContent,
  SelectItem
} from './ui/select'

type DropdownProps = {
  label: string
  options: string[]
  currentValue: string | undefined
  onChange: (value: string) => void
}

export function DropdownMenu({
  label,
  options,
  currentValue,
  onChange
}: DropdownProps) {
  return (
    <Select onValueChange={onChange}>
      <SelectTrigger className="w-full border-0 border-none bg-blue-900/15 p-6 text-xl font-medium !text-white focus:!ring-0">
        <SelectValue placeholder={label} value={currentValue} />
      </SelectTrigger>
      <SelectContent className="border-none bg-slate-900 text-xl">
        {options.map((option) => (
          <SelectItem
            key={option}
            value={option}
            className="border-none bg-slate-900 text-xl font-medium text-white focus:bg-slate-800/30 focus:text-white"
          >
            {option}
          </SelectItem>
        ))}
      </SelectContent>
    </Select>
  )
}
