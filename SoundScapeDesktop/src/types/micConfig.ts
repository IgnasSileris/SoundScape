export type MicConfigId = string

export type MicDeviceOption = {
  id: string
  name: string
}

export type FilterOption = {
  id: string
  name: string
}

export type MicConfig = {
  id: MicConfigId
  name: string
  inputDeviceId: string
  outputDeviceId: string
  reduceBackgroundNoise: boolean
  filterId?: string
}

export type MicConfigDraft = {
  name: string
  inputDeviceId?: string
  outputDeviceId?: string
  reduceBackgroundNoise: boolean
  filterId?: string
}
