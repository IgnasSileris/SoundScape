import { CheckCircle } from 'lucide-react'
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogHeader,
  DialogDescription,
  DialogFooter
} from './ui/dialog'
import { Button } from './ui/button'

type ConfirmationModalProps = {
  isOpen: boolean
  message: string
  onConfirm: () => void
  onCancel: () => void
}
const ConfirmationModal = ({
  isOpen,
  message,
  onConfirm,
  onCancel
}: ConfirmationModalProps) => {
  return (
    <Dialog
      open={isOpen}
      onOpenChange={(isDialogOpen) => !isDialogOpen && onCancel()}
    >
      <DialogContent className="border border-green-500/30 bg-slate-950 text-white shadow-2xl shadow-green-950/30 sm:max-w-md">
        <DialogHeader className="gap-4">
          <div className="flex items-center gap-3">
            <span className="flex size-10 items-center justify-center rounded-full bg-red-500/10 text-green-400">
              <CheckCircle className="size-5" />
            </span>
            <DialogTitle className="text-xl font-medium text-white">
              Confirm
            </DialogTitle>
          </div>
          <DialogDescription className="text-base leading-7 text-slate-300">
            {message}
          </DialogDescription>
        </DialogHeader>
        <DialogFooter className="justify-center sm:justify-center">
          <div className="flex w-4/5 justify-between">
            <Button
              onClick={onConfirm}
              className="rounded-full bg-green-500 px-6 text-slate-950 hover:bg-green-400"
            >
              Yes
            </Button>
            <Button
              onClick={onCancel}
              className="rounded-full bg-red-500 px-6 text-slate-950 hover:bg-red-400"
            >
              No
            </Button>
          </div>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  )
}

export default ConfirmationModal
