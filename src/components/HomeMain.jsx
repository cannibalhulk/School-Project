import '../styles/HomeMain.css'
import Filter from './Filter'

function HomeMain() {

  return (
    <div id='home'>
      <div id='midThings'>
        <h1 id='homeText'>Kitab axtarışı</h1>
        <form action="/">
          <input type="text" placeholder='Kitab adını daxil edin'/>
          <input type="submit" value={"Axtar"}/>
        </form>
      </div>
      <Filter/>
    </div>
  )
}

export default HomeMain
