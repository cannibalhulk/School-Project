import '../styles/HomeMain.css'

function HomeMain() {

  return (
    <div id='home'>
      <div id='midThings'>
        <h1 id='homeText'>Kitab axtarışı</h1>
        <form action="/">
          <input type="text" placeholder='Kitab adı'/>
          <input type="submit" value={"Axtar"}/>
        </form>
      </div>

      <div id='books'>
        <div id='grid-container'>
          <div className='grid-item'>book</div>
          <div className='grid-item'>book</div>
          <div className='grid-item'>book</div>
          <div className='grid-item'>book</div>
          <div className='grid-item'>book</div>
          <div className='grid-item'>book</div>
        </div>
      </div>
    </div>
  )
}

export default HomeMain
