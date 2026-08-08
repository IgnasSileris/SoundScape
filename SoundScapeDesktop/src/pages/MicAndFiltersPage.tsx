import { useMemo, useState } from 'react'
import ConfigSelector from '../components/ConfigSelector'
import MicConfigForm from '../components/MicConfigForm'
import Recordbutton from '../components/RecordButton'
import type { MicConfig, MicConfigDraft } from '../types'
import {
  useDeleteMicConfig,
  useMicConfigs,
  useSaveMicConfig
} from '../hooks/useConfigurations'
import ErrorModal from '../components/ErrorModal'
import ConfirmationModal from '../components/ConfirmationModal'

const NEW_CONFIG_VALUE = 'new'

const createEmptyDraft = (): MicConfigDraft => {
  return {
    name: 'Untitled configuration',
    inputDeviceId: undefined,
    outputDeviceId: undefined,
    reduceBackgroundNoise: false,
    filterId: undefined
  }
}

const createDraftFromConfig = (config: MicConfig): MicConfigDraft => {
  return {
    name: config.name,
    inputDeviceId: config.inputDeviceId,
    outputDeviceId: config.outputDeviceId,
    reduceBackgroundNoise: config.reduceBackgroundNoise,
    filterId: config.filterId
  }
}

const MicAndFiltersPage = () => {
  const { data: savedConfigs = [] } = useMicConfigs()
  const saveMicConfig = useSaveMicConfig()
  const deleteMicConfig = useDeleteMicConfig()

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

  const [currentErrorMessage, setCurrentErrorMessage] = useState<
    string | undefined
  >(undefined)
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState<boolean>(false)

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

  const handleSaveConfig = async () => {
    if (
      currentConfig.name.trim().length === 0 ||
      !currentConfig.inputDeviceId ||
      !currentConfig.outputDeviceId
    ) {
      return
    }

    let newConfig: MicConfig

    if (isCreatingNewConfig) {
      newConfig = {
        id: `config-${Date.now()}`,
        name: currentConfig.name.trim(),
        inputDeviceId: currentConfig.inputDeviceId,
        outputDeviceId: currentConfig.outputDeviceId,
        reduceBackgroundNoise: currentConfig.reduceBackgroundNoise,
        filterId: currentConfig.filterId
      }
    } else {
      if (!selectedConfig) {
        return
      }

      newConfig = {
        id: selectedConfig.id,
        name: currentConfig.name.trim(),
        inputDeviceId: currentConfig.inputDeviceId,
        outputDeviceId: currentConfig.outputDeviceId,
        reduceBackgroundNoise: currentConfig.reduceBackgroundNoise,
        filterId: currentConfig.filterId
      }
    }

    const isSaved = await saveMicConfig.mutateAsync(newConfig)

    if (!isSaved) {
      setCurrentErrorMessage(
        'There was an error trying to save the configuration.'
      )
      return
    }
    setSelectedConfigId(newConfig.id)
    setCurrentConfig(createDraftFromConfig(newConfig))
  }

  const handleDeleteConfig = async () => {
    if (!selectedConfigId) {
      return
    }

    if (isCreatingNewConfig) {
      setCurrentConfig(createEmptyDraft())
      setSelectedConfigId(undefined)
      return
    }

    const isDeleted = await deleteMicConfig.mutateAsync(selectedConfigId)

    if (!isDeleted) {
      setCurrentErrorMessage(
        'There was an error trying to delete the configuration.'
      )
      return
    }
    setCurrentConfig(createEmptyDraft())
    setSelectedConfigId(undefined)
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
            onDelete={() => setIsDeleteModalOpen(true)}
          />
        )}
      </div>

      <div className="mt-auto flex w-full items-center justify-center pt-4">
        <Recordbutton />
      </div>
      <ErrorModal
        isOpen={currentErrorMessage !== undefined}
        message={currentErrorMessage ?? ''}
        onClose={() => setCurrentErrorMessage(undefined)}
      />
      <ConfirmationModal
        isOpen={isDeleteModalOpen}
        message={`Are you sure you want to delete configuration '${currentConfig.name}'?`}
        onCancel={() => setIsDeleteModalOpen(false)}
        onConfirm={async () => {
          await handleDeleteConfig()
          setIsDeleteModalOpen(false)
        }}
      />
    </div>
  )
}

export default MicAndFiltersPage
