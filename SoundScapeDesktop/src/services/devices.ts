import type { MicDeviceOption } from '../types'

export const fetchInputMics = async (): Promise<MicDeviceOption[]> => {
  const devices = window.soundScapeApi.getInputMics()
  return devices ?? []
}

export const fetchOutputMics = async (): Promise<MicDeviceOption[]> => {
  const devices = window.soundScapeApi.getOutputMics()
  return devices ?? []
}
