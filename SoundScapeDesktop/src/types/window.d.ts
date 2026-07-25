import type {
  MicConfigId,
  MicConfig,
  MicDeviceOption,
  AudioStateRequest
} from './index'

declare global {
  interface Window {
    soundScapeApi: {
      getInputMics: () => Promise<MicDeviceOption[]>
      getOutputMics: () => Promise<MicDeviceOption[]>

      getAllConfigs: () => Promise<MicConfig[]>
      saveConfig: (payload: MicConfig) => Promise<boolean>
      deleteConfig: (payload: MicConfigId) => Promise<boolean>

      updateAudioState: (payload: AudioStateRequest) => Promise<boolean>
    }
  }
}

export {}
