import type { MicDeviceOption } from '../types'

export const fetchInputMics = async (): Promise<MicDeviceOption[]> => {
  const devices = await window.soundScapeApi.getInputMics()
  return devices ?? []
}

export const fetchOutputMics = async (): Promise<MicDeviceOption[]> => {
  const devices = await window.soundScapeApi.getOutputMics()
  return devices ?? []
}
