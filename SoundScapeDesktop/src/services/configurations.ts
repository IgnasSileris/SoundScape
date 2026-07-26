import type { MicConfig, MicConfigId } from '../types'

export const fetchMicConfigs = async (): Promise<MicConfig[]> => {
  const configs = await window.soundScapeApi.getAllConfigs()
  return configs ?? []
}

export const saveMicConfig = async (newConfig: MicConfig): Promise<boolean> => {
  const isSaved = await window.soundScapeApi.saveConfig(newConfig)
  return isSaved
}

export const deleteMicConfig = async (
  configId: MicConfigId
): Promise<boolean> => {
  const isDeleted = await window.soundScapeApi.deleteConfig(configId)
  return isDeleted
}
