import React from 'react';
import { Post } from './Post';

export function Wall(props) {
  var posts = props.posts;

  let marx;

  if (posts === null || posts === undefined) {
    marx = <div>
      <p>No Posts Available</p>
    </div>;
  } else {
    marx = posts.map(c => (
      <Post post={c} />
    ));
  }

  return <section id="PersonalTimeline" className="rounded border shadow p-1 col-md-6">
    {marx}
  </section>;
}
