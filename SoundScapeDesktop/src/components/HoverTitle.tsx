import type { ReactNode } from 'react'

type HoverTitleProps = {
  title?: string
  children: ReactNode
}

const HoverTitle = ({ title, children }: HoverTitleProps) => {
  return (
    <span className="group relative inline-flex">
      {children}
      {title && (
        <span className="pointer-events-none absolute bottom-full left-1/2 z-10 mb-2 -translate-x-1/2 rounded-md bg-slate-800 px-3 py-1.5 text-sm font-medium whitespace-nowrap text-white opacity-0 shadow-lg transition group-hover:opacity-100">
          {title}
        </span>
      )}
    </span>
  )
}

export default HoverTitle
