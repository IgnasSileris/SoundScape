import { CircleHelp, Trash2Icon } from 'lucide-react'
import type { FilterOption, MicConfigDraft } from '../types'
import DropdownMenu from './DropdownMenu'
import { Button } from './ui/button'
import { useInputMics, useOutputMics } from '../hooks/useDevices'
import HoverTitle from './HoverTitle'

const FILTER_OPTIONS: FilterOption[] = [
  { id: 'noise-reduction', name: 'Noise Reduction' },
  { id: 'voice-deepening', name: 'Voice Deepening' }
]

const EMPTY_FILTER_VALUE = '__no-filter__'
const EMPTY_INPUT_MICS_VALUE = '__no-input-mics__'
const EMPTY_OUTPUT_MICS_VALUE = '__no-output-mics__'

type MicConfigFormProps = {
  config: MicConfigDraft
  onConfigChange: (updates: Partial<MicConfigDraft>) => void
  onSave: () => void
  onDelete: () => void
}

const InputLabel = ({ label, hint }: { label: string; hint: string }) => {
  return (
    <div className="flex items-center gap-2">
      <h3 className="text-lg">{label}</h3>
      <HoverTitle title={hint}>
        <span className="inline-flex cursor-help text-slate-400 transition hover:text-slate-200">
          <CircleHelp className="h-4 w-4" />
        </span>
      </HoverTitle>
    </div>
  )
}

const MicConfigForm = ({
  config,
  onConfigChange,
  onSave,
  onDelete
}: MicConfigFormProps) => {
  const { data: inputMics = [] } = useInputMics()
  const { data: outputMics = [] } = useOutputMics()

  const isSaveDisabled =
    config.name.trim().length === 0 ||
    !config.inputDeviceId ||
    !config.outputDeviceId

  return (
    <div className="flex w-full max-w-[760px] flex-col gap-6">
      <div className="flex w-full flex-col items-start gap-3">
        <InputLabel
          label="Input microphone"
          hint="Choose the input device this configuration should listen to."
        />
        <DropdownMenu
          label="Select an input microphone..."
          options={
            inputMics.length === 0
              ? [
                  {
                    value: EMPTY_INPUT_MICS_VALUE,
                    label: 'No input microphones found',
                    className: 'font-normal text-white/60',
                    disabled: true
                  }
                ]
              : inputMics.map((option) => ({
                  value: option.id,
                  label: option.name
                }))
          }
          currentValue={
            inputMics.find((option) => option.id === config.inputDeviceId)?.id
          }
          onChange={(value) => onConfigChange({ inputDeviceId: value })}
        />
      </div>

      <div className="flex w-full flex-col items-start gap-3">
        <InputLabel
          label="Output microphone"
          hint="Choose the virtual output device this configuration should send audio to."
        />
        <DropdownMenu
          label="Select an output microphone..."
          options={
            outputMics.length === 0
              ? [
                  {
                    value: EMPTY_OUTPUT_MICS_VALUE,
                    label: 'No virtual microphones found',
                    className: 'font-normal text-white/60',
                    disabled: true
                  }
                ]
              : outputMics.map((option) => ({
                  value: option.id,
                  label: option.name
                }))
          }
          currentValue={
            outputMics.find((option) => option.id === config.outputDeviceId)?.id
          }
          onChange={(value) => onConfigChange({ outputDeviceId: value })}
        />
      </div>

      <div className="flex w-full flex-col items-start gap-3">
        <InputLabel
          label="Filter"
          hint="Apply an effect to this configuration."
        />
        <DropdownMenu
          label="Select a filter..."
          options={[
            {
              value: EMPTY_FILTER_VALUE,
              label: 'Select a filter...',
              className: 'font-normal text-white/60'
            },
            ...FILTER_OPTIONS.map((option) => ({
              value: option.id,
              label: option.name
            }))
          ]}
          currentValue={config.filterId ?? EMPTY_FILTER_VALUE}
          onChange={(value) => {
            onConfigChange({
              filterId: value === EMPTY_FILTER_VALUE ? undefined : value
            })
          }}
        />
      </div>

      <label className="flex items-center gap-4 px-1 py-2 text-left">
        <input
          type="checkbox"
          checked={config.reduceBackgroundNoise}
          onChange={(event) =>
            onConfigChange({
              reduceBackgroundNoise: event.target.checked
            })
          }
          className="size-5 accent-sky-500"
        />
        <div className="flex items-center gap-2">
          <span className="text-base font-medium">Reduce background noise</span>
          <HoverTitle title="Clean up room tone and low-level background sound.">
            <span className="inline-flex cursor-help text-slate-400 transition hover:text-slate-200">
              <CircleHelp className="size-4" />
            </span>
          </HoverTitle>
        </div>
      </label>

      <div className="flex w-full items-center justify-end gap-4">
        <HoverTitle title="Delete configuration">
          <Button
            onClick={onDelete}
            className="bg-transparent text-slate-300 shadow-none hover:bg-transparent hover:text-red-500"
          >
            <Trash2Icon className="size-6" />
          </Button>
        </HoverTitle>
        <HoverTitle
          title={
            isSaveDisabled
              ? 'Name, input microphone, and output microphone are required.'
              : undefined
          }
        >
          <Button
            onClick={onSave}
            disabled={isSaveDisabled}
            className="rounded-full bg-sky-500 px-6 text-slate-950 hover:bg-sky-400"
          >
            Save configuration
          </Button>
        </HoverTitle>
      </div>
    </div>
  )
}

export default MicConfigForm
