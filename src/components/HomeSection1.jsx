import { useState } from 'react'
import '../styles/HomeSection1.css'

function HomeSection1() {

  let [booksVisible,setBooksVisible] = useState(false);

  return (
    booksVisible &&
    (<div id='books'>
      <div id='grid-container'>
        <div className='grid-item'>book</div>
        <div className='grid-item'>book</div>
        <div className='grid-item'>book</div>
        <div className='grid-item'>book</div>
        <div className='grid-item'>book</div>
        <div className='grid-item'>book</div>
      </div>
    </div>)
  )
}

export default HomeSection1
