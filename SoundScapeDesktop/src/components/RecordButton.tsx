import { IconMicrophoneFilled, IconPlayerStopFilled } from '@tabler/icons-react'
import { Button } from './ui/button'
import { useState } from 'react'

export default function Recordbutton() {
  // Current mic and filter selection to be stored in the backend so no need to pass in again
  // might also need to add intermediate processing state (to show that is loading)
  // backend will need to record both the unfiltered and filtered versions.
  const [isRecording, setIsRecording] = useState<boolean>(false)

  const startRecording = () => {
    // backend call
    setIsRecording(true)
  }

  const stopRecording = () => {
    // backend call
    setIsRecording(false)
  }
  return (
    <>
      {isRecording ? (
        <Button
          className="h-16 w-40 rounded-3xl bg-blue-600 text-xl hover:bg-blue-700"
          onClick={() => stopRecording()}
        >
          <IconPlayerStopFilled style={{ width: '22px', height: '22px' }} />{' '}
          Stop
        </Button>
      ) : (
        <Button
          className="h-16 w-40 rounded-3xl bg-blue-600 text-xl hover:bg-blue-700"
          onClick={() => startRecording()}
        >
          <IconMicrophoneFilled style={{ width: '22px', height: '22px' }} />{' '}
          Record
        </Button>
      )}
    </>
  )
}
