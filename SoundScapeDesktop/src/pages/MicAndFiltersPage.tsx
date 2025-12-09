import { useState } from 'react'
import { DropdownMenu } from '../components/DropdownMenu'
import Recordbutton from '../components/RecordButton'

const FILTERS = ['Noise Reduction', 'Voice Deepening']

export default function MicAndFiltersPage() {
  const [selectedFilter, setSelectedFilter] = useState<string | undefined>(
    undefined
  )
  const [selectedMic, setSelectedMic] = useState<string | undefined>(undefined)
  return (
    <div className="flex h-full w-full flex-col items-start gap-4 p-[12%] text-white">
      <h1 className="!text-4xl">Microphone</h1>
      <span></span>
      <div className="flex w-full flex-col items-start gap-6">
        <div className="flex w-full flex-col items-start gap-5">
          <h2 className="!text-2xl font-light">Microphone</h2>
          <DropdownMenu
            label="Select a microphone..."
            options={['Mic1', 'Mic2']}
            currentValue={selectedMic}
            onChange={setSelectedMic}
          />
        </div>
        <div className="flex w-full flex-col items-start gap-5">
          <h2 className="!text-2xl font-light">Filter</h2>
          <DropdownMenu
            label="Select a filter..."
            options={FILTERS}
            currentValue={selectedFilter}
            onChange={setSelectedFilter}
          />
        </div>
      </div>
      <div className="h-[100px] w-full"> </div>
      <div className="flex w-full items-center justify-center">
        <Recordbutton />
      </div>
    </div>
  )
}
