import { Pencil } from 'lucide-react'
import { useEffect, useState } from 'react'
import type { MicConfig } from '../types'
import { Select, SelectContent, SelectItem, SelectTrigger } from './ui/select'

type ConfigSelectorProps = {
  configs: MicConfig[]
  selectedConfigId?: string
  currentName: string
  newConfigValue: string
  onSelect: (value: string) => void
  onConfigChange: (updates: { name: string }) => void
}

const ConfigSelector = ({
  configs,
  selectedConfigId,
  currentName,
  newConfigValue,
  onSelect,
  onConfigChange
}: ConfigSelectorProps) => {
  const hasActiveConfig = selectedConfigId !== undefined
  const [isRenaming, setIsRenaming] = useState(false)

  useEffect(() => {
    setIsRenaming(selectedConfigId === newConfigValue)
  }, [newConfigValue, selectedConfigId])

  return (
    <div className="flex w-full max-w-[640px] flex-col gap-3">
      <div className="flex items-center justify-between gap-3">
        <span className="text-sm font-medium tracking-[0.2em] text-slate-400 uppercase">
          Saved configurations
        </span>
        {hasActiveConfig && (
          <button
            type="button"
            className="flex items-center gap-2 rounded-full border border-slate-700 px-3 py-1 text-sm text-slate-300 transition hover:border-slate-500 hover:text-white"
            onClick={() => setIsRenaming(true)}
          >
            <Pencil className="h-3.5 w-3.5" />
            Edit name
          </button>
        )}
      </div>

      <div className="flex flex-col gap-3">
        {isRenaming && hasActiveConfig ? (
          <input
            value={currentName}
            onChange={(event) => onConfigChange({ name: event.target.value })}
            onBlur={() => setIsRenaming(false)}
            autoFocus
            className="w-full rounded-2xl border border-slate-700 bg-blue-900/15 px-5 py-4 text-xl font-medium text-white transition outline-none focus:border-sky-400"
          />
        ) : (
          <Select value={selectedConfigId} onValueChange={onSelect}>
            <SelectTrigger className="w-full rounded-2xl border-0 bg-blue-900/15 px-5 py-8 text-left text-xl font-medium text-white">
              <span className="truncate">
                {hasActiveConfig
                  ? currentName
                  : 'Select a custom configuration...'}
              </span>
            </SelectTrigger>
            <SelectContent className="border-none bg-slate-900 text-xl">
              {configs.map((config) => (
                <SelectItem
                  key={config.id}
                  value={config.id}
                  className="bg-slate-900 text-xl font-medium text-white focus:bg-slate-800/30 focus:text-white"
                >
                  {config.name}
                </SelectItem>
              ))}
              <SelectItem
                value={newConfigValue}
                className="bg-slate-900 text-xl font-medium text-sky-300 focus:bg-slate-800/30 focus:text-sky-200"
              >
                + New configuration
              </SelectItem>
            </SelectContent>
          </Select>
        )}
      </div>
    </div>
  )
}

export default ConfigSelector
