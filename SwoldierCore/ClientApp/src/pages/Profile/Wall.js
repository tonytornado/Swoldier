import React from 'react';

export function Wall(props) {
  var posts = props.posts;

  let marx;

  if (posts === null || posts === undefined) {
    marx = <div>
      <p>No Posts Available</p>
    </div>;
  } else {
    marx = posts.map(c => (
      <div key={c.postId} className="rounded border p-3 m-3">
        <p>{c.postText}</p>
      </div>
    ));
  }

  return <section id="PersonalTimeline" className="rounded border shadow p-1 col-md-6">
    {marx}
  </section>;
}
