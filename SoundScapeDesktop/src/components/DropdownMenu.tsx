import { Select, SelectTrigger, SelectContent, SelectItem } from './ui/select'

type DropdownOption = {
  value: string
  label: string
  className?: string
}

type DropdownProps = {
  label: string
  options: Array<string | DropdownOption>
  currentValue: string | undefined
  onChange: (value: string) => void
}

export function DropdownMenu({
  label,
  options,
  currentValue,
  onChange
}: DropdownProps) {
  const normalizedOptions = options.map((option) =>
    typeof option === 'string' ? { value: option, label: option } : option
  )

  const currentOption = normalizedOptions.find(
    (option) => option.value === currentValue
  )

  return (
    <Select value={currentValue} onValueChange={onChange}>
      <SelectTrigger className="!focus:ring-0 w-full border-0 border-none bg-blue-900/15 p-6 text-xl font-medium text-white!">
        {currentOption ? (
          <span className={currentOption.className}>{currentOption.label}</span>
        ) : (
          <span className="font-normal text-white/60">{label}</span>
        )}
      </SelectTrigger>
      <SelectContent className="border-none bg-slate-900 text-xl">
        {normalizedOptions.map((option) => (
          <SelectItem
            key={option.value}
            value={option.value}
            className="border-none bg-slate-900 text-xl font-medium text-white focus:bg-slate-800/30 focus:text-white"
          >
            <span className={option.className}>{option.label}</span>
          </SelectItem>
        ))}
      </SelectContent>
    </Select>
  )
}
