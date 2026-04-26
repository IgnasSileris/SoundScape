import { useMemo, useState } from 'react'
import ConfigSelector from '../components/ConfigSelector'
import MicConfigForm from '../components/MicConfigForm'
import Recordbutton from '../components/RecordButton'
import type { MicConfig, MicConfigDraft } from '../types'

const NEW_CONFIG_VALUE = 'new'

const INITIAL_SAVED_CONFIGS: MicConfig[] = [
  {
    id: 'config-streaming',
    name: 'Streaming setup',
    inputMicId: 'shure-mv7',
    outputMicId: 'obs-virtual',
    reduceBackgroundNoise: true,
    filterId: 'noise-reduction'
  },
  {
    id: 'config-meetings',
    name: 'Meetings',
    inputMicId: 'rode-nt-usb',
    outputMicId: 'discord-virtual',
    reduceBackgroundNoise: false
  }
]

const createEmptyDraft = (): MicConfigDraft => {
  return {
    name: 'Untitled configuration',
    inputMicId: undefined,
    outputMicId: undefined,
    reduceBackgroundNoise: false,
    filterId: undefined
  }
}

const createDraftFromConfig = (config: MicConfig): MicConfigDraft => {
  return {
    name: config.name,
    inputMicId: config.inputMicId,
    outputMicId: config.outputMicId,
    reduceBackgroundNoise: config.reduceBackgroundNoise,
    filterId: config.filterId
  }
}

const MicAndFiltersPage = () => {
  const [savedConfigs, setSavedConfigs] = useState<MicConfig[]>(
    INITIAL_SAVED_CONFIGS
  )
  const [selectedConfigId, setSelectedConfigId] = useState<string | undefined>(
    undefined
  )
  const [currentConfig, setCurrentConfig] =
    useState<MicConfigDraft>(createEmptyDraft())

  const selectedConfig = useMemo(
    () => savedConfigs.find((config) => config.id === selectedConfigId),
    [savedConfigs, selectedConfigId]
  )

  const hasActiveConfig = selectedConfigId !== undefined
  const isCreatingNewConfig = selectedConfigId === NEW_CONFIG_VALUE

  const handleConfigSelection = (value: string) => {
    setSelectedConfigId(value)

    if (value === NEW_CONFIG_VALUE) {
      setCurrentConfig(createEmptyDraft())
      return
    }

    const nextConfig = savedConfigs.find((config) => config.id === value)

    if (!nextConfig) {
      return
    }

    setCurrentConfig(createDraftFromConfig(nextConfig))
  }

  const handleConfigChange = (updates: Partial<MicConfigDraft>) => {
    setCurrentConfig((previousConfig) => ({
      ...previousConfig,
      ...updates
    }))
  }

  const handleSaveConfig = () => {
    if (
      currentConfig.name.trim().length === 0 ||
      !currentConfig.inputMicId ||
      !currentConfig.outputMicId
    ) {
      return
    }

    if (isCreatingNewConfig) {
      const newConfig: MicConfig = {
        id: `config-${Date.now()}`,
        name: currentConfig.name.trim(),
        inputMicId: currentConfig.inputMicId,
        outputMicId: currentConfig.outputMicId,
        reduceBackgroundNoise: currentConfig.reduceBackgroundNoise,
        filterId: currentConfig.filterId
      }

      setSavedConfigs((currentConfigs) => [...currentConfigs, newConfig])
      setSelectedConfigId(newConfig.id)
      setCurrentConfig(createDraftFromConfig(newConfig))
      return
    }

    if (!selectedConfig) {
      return
    }

    const updatedConfig: MicConfig = {
      ...selectedConfig,
      name: currentConfig.name.trim(),
      inputMicId: currentConfig.inputMicId,
      outputMicId: currentConfig.outputMicId,
      reduceBackgroundNoise: currentConfig.reduceBackgroundNoise,
      filterId: currentConfig.filterId
    }

    setSavedConfigs((currentConfigs) =>
      currentConfigs.map((config) =>
        config.id === selectedConfig.id ? updatedConfig : config
      )
    )
    setCurrentConfig(createDraftFromConfig(updatedConfig))
  }

  return (
    <div className="flex h-full w-full flex-col gap-10 overflow-auto px-[10%] py-[8%] text-white">
      <div className="flex w-full flex-col items-center gap-5">
        <ConfigSelector
          configs={savedConfigs}
          selectedConfigId={selectedConfigId}
          currentName={currentConfig.name}
          newConfigValue={NEW_CONFIG_VALUE}
          onSelect={handleConfigSelection}
          onConfigChange={handleConfigChange}
        />

        {hasActiveConfig && (
          <MicConfigForm
            config={currentConfig}
            onConfigChange={handleConfigChange}
            onSave={handleSaveConfig}
          />
        )}
      </div>

      <div className="mt-auto flex w-full items-center justify-center pt-4">
        <Recordbutton />
      </div>
    </div>
  )
}

export default MicAndFiltersPage
